using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
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
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<Guid> Create([FromBody] TaskModel task, CancellationToken cancellationToken)
        {
            var taskEntity = task.MapObject<TaskModel, TaskEntity>();
            await _repository.AddAsync(taskEntity, cancellationToken);
            return taskEntity.Id;
        }


        [HttpPost("upload-task-files")]
        [Authorize(Policy = "Task_Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadFile([Required] List<IFormFile> files, [Required] Guid taskId, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(taskId, cancellationToken);
            var filePaths = await _fileService.UploadFileAsync(files, taskId.ToString(), cancellationToken);
            task.Files = new(filePaths);
            await _repository.UpdateAsync(task, cancellationToken);
            return Ok();
        }


        [HttpGet("download-task-files")]
        [Authorize]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public IActionResult Download([Required] string directory, CancellationToken cancellationToken)
        {
            var (fileType, archiveData, archiveName) = _fileService.DownloadFiles(directory, cancellationToken);
            return File(archiveData, fileType, archiveName);
        }


        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(TaskResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<TaskResponseModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(id, cancellationToken);
            return task.MapObject<TaskEntity, TaskResponseModel>();
        }


        [HttpGet("all")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TaskResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<TaskResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            var tasks = await _repository.GetAllAsync(cancellationToken);
            return tasks.MapList<TaskEntity, TaskResponseModel>();
        }


        [HttpPut("edit")]
        [Authorize(Policy = "Task_Update")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit([FromBody] TaskModel task, [FromQuery] Guid taskId, CancellationToken cancellationToken)
        {
            var taskEntity = await _repository.GetByIdAsync(taskId, cancellationToken);
            taskEntity.Title = task.Title;
            taskEntity.ShortDescription = task.ShortDescription;
            taskEntity.Description = task.Description;
            taskEntity.UserName = task.UserName;
            return Ok(await _repository.UpdateAsync(taskEntity, cancellationToken));
        }


        [HttpDelete("delete")]
        [Authorize(Policy = "Task_Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTask(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
            return  NoContent();
        }
    }
}
