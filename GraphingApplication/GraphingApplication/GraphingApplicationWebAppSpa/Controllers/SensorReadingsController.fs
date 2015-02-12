namespace FsWeb.Controllers
 
open System.Web.Http
open FsWeb.Models
open FsWeb.Repositories
 
type SensorReadingsController(repository : SensorReadingsRepository) =
    inherit ApiController()
 
    new() = new SensorReadingsController(new SensorReadingsRepository())
 
    member x.Get() = 
        repository.GetAll()
