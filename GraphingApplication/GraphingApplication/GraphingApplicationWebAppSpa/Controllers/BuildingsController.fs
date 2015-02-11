namespace FsWeb.Controllers
 
open System.Web.Http
open FsWeb.Models
open FsWeb.Repositories
 
type BuildingsController(repository : BuildingsRepository) =
    inherit ApiController()
 
    new() = new BuildingsController(new BuildingsRepository())
 
    member x.Get() = 
        repository.GetAll()
 
    member x.Post ([<FromBody>] contact) =    
        repository.AddNew(contact)