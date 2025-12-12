using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeLavant.Domain.Lectures;

namespace DeLavant.Domain.MediaFiles
{
    public interface IMediaFileRepository
    {
        Task<List<MediaFile>> GetMediaFilesByListIdsAsync(List<string> MediaFilesIds);
        Task<MediaFile?> GetMediaFileByIdAsync(string id);
        Task CreateMediaFileAsync(MediaFile mediaFile);
        Task UpdateMediaFileAsync(MediaFile mediaFile);
        Task DeleteMediaFileAsync(string id);
    }
}
