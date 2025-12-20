namespace DeLavantTechnologies.Data
{
    public enum AdditionalPageType
    {
        Access,
        Statistic,
        Delete
    }

    public class CourseEditUiState
    {
        /* ===== PANELS ===== */

        public bool ShowLeft { get; private set; }
        public bool ShowRight { get; private set; }
        public bool ShowAdditional { get; private set; }

        public AdditionalPageType Type { get; private set; } = AdditionalPageType.Access;

        public event Action? Changed;

        /* ===== LEFT / RIGHT ===== */

        public void ToggleLeft()
        {
            ShowLeft = !ShowLeft;
            ShowRight = false;
            ShowAdditional = false;
            Notify();
        }

        public void ToggleRight()
        {
            ShowRight = !ShowRight;
            ShowLeft = false;
            ShowAdditional = false;
            Notify();
        }

        /* ===== ADDITIONAL (CENTER) ===== */

        public void OpenAdditional(AdditionalPageType type)
        {
            Type = type;
            ShowAdditional = true;

            ShowLeft = false;
            ShowRight = false;

            Notify();
        }

        public void CloseAdditional()
        {
            Type = AdditionalPageType.Access;
            ShowAdditional = false;
            Notify();
        }

        private void Notify() => Changed?.Invoke();
    }
}
