using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TMS.Application.Interfaces;
using TMS.Application.Mappings;
using TMS.Application.Models;
using TMS.Domain.Entities;

namespace TMS.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IRepository<TaskEntity, Guid> _repository;
        private readonly IFileService _fileService;

        public TaskController(IRepository<TaskEntity, Guid> repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        [HttpPost("create")]
        [Authorize(Policy = "Task_Create")]
        public async Task<Guid> Create([FromBody] TaskModel task)
        {
            var taskEntity = task.MapObject<TaskModel, TaskEntity>();
            await _repository.AddAsync(taskEntity);
            return taskEntity.Id;
        }


        [HttpPost("upload-task-files")]
        [Authorize(Policy = "Task_Create")]
        public async Task<IActionResult> UploadFile([Required] List<IFormFile> files, [Required] Guid taskId)
        {
            var task = await _repository.GetByIdAsync(taskId);
            var filePaths = await _fileService.UploadFileAsync(files, taskId.ToString());
            task.Files = new(filePaths);
            await _repository.UpdateAsync(task);
            return Ok();
        }


        [HttpGet("download-task-files")]
        [Authorize]
        public IActionResult Download([Required] string directory)
        {
            var (fileType, archiveData, archiveName) = _fileService.DownloadFiles(directory);
            return File(archiveData, fileType, archiveName);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<TaskResponseModel> GetById(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            return task.MapObject<TaskEntity, TaskResponseModel>();
        }


        [HttpGet("all")]
        [Authorize]
        public async Task<IEnumerable<TaskResponseModel>> GetAll()
        {
            var tasks = await _repository.GetAllAsync();
            return tasks.MapList<TaskEntity, TaskResponseModel>();
        }


        [HttpPut("edit")]
        [Authorize(Policy = "Task_Update")]
        public async Task<bool> Edit([FromBody] TaskModel task, [FromQuery] Guid taskId)
        {
            var taskEntity = await _repository.GetByIdAsync(taskId);
            taskEntity.Title = task.Title;
            taskEntity.ShortDescription = task.ShortDescription;
            taskEntity.Description = task.Description;
            taskEntity.UserName = task.UserName;
            return await _repository.UpdateAsync(taskEntity);
        }


        [HttpDelete("delete")]
        [Authorize(Policy = "Task_Delete")]
        public async Task<bool> DeleteTask(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
