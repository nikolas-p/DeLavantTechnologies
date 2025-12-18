using Microsoft.AspNetCore.Authorization;

namespace DeLavantTechnologies.Data
{
    public enum EntityType
    {
        DestinationCourses,
        Courses,
        Users,
        Positions,
        Applicants,
        Projects,
        Home,
        Policy
    }
    public enum PageMode
    {
        List,
        Edit
    }


    public class EntityState
    {
        public string Search { get; set; } = "";

        // Любые параметры Aside
        public Dictionary<string, object> Parameters { get; } = new();

        public void Reset()
        {
            Search = "";
            Parameters.Clear();
        }
    }


    public class UiState
    {
        public EntityType Entity { get; private set; } = EntityType.DestinationCourses;
        public PageMode Mode { get; private set; } = PageMode.List;

        // Панели (по умолчанию закрыты)
        public bool ShowLeft { get; private set; } = false;
        public bool ShowRight { get; private set; } = false;
        public bool ShowSearch {get; private set; } = false;
        public bool ShowLayerOnTop { get; private set; } = false;

        private readonly Dictionary<EntityType, EntityState> _entityStates = new();

        public EntityState CurrentState
        {
            get
            {
                if (!_entityStates.TryGetValue(Entity, out var state))
                {
                    state = new EntityState();
                    _entityStates[Entity] = state;
                }
                return state;
            }
        }


        public event Action? Changed;

        /* =========================
           Навигация
        ========================= */
        public UiState()
        {
            Entity = EntityType.DestinationCourses;
        }
        public void SelectEntity(EntityType entity)
        {
            if (Entity == entity) return;

            Entity = entity;
            Mode = PageMode.List;

            // ⬇ сбрасываем состояние НОВОЙ entity
            CurrentState.Reset();

            ClosePanels();
            Notify();
        }


        public void SetMode(PageMode mode)
        {
            Mode = mode;

            // В режиме Edit/Create панели не нужны
            if (mode != PageMode.List)
                ClosePanels();

            Notify();
        }

        /* =========================
           Панели
        ========================= */

        public void ToggleLeft()
        {
            ShowLeft = !ShowLeft;
            ShowSearch = false;
            ShowRight = false; // нельзя открыть обе
            Notify();
        }

        public void ToggleRight()
        {
            ShowRight = !ShowRight;
            ShowSearch = false;
            ShowLeft = false;
            Notify();
        }

        public void ToggleSearch()
        {
            ShowSearch = !ShowSearch;
            Notify();
        }

        public void ClosePanels()
        {
            if (!ShowLeft && !ShowRight) return;

            ShowLeft = false;
            ShowRight = false;
            Notify();
        }

        /* =========================
           Фильтры / поиск
        ========================= */

        public void SetSearch(string value)
        {
            CurrentState.Search = value;
            Notify();
        }


        public void SetParam(string key, object value)
        {
            CurrentState.Parameters[key] = value;
            Notify();
        }


        private void Notify() => Changed?.Invoke();
    }

    public interface IEntityListPage
    {
        string Search { get; set; }
        Dictionary<string, object> Parameters { get; set; }
    }


}
