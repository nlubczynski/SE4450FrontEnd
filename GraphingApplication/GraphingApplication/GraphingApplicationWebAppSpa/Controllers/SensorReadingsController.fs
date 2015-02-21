namespace FsWeb.Controllers
 
open System.Web.Http
open FsWeb.Models
open FsWeb.Repositories
 
type SensorReadingsController(repository : SensorReadingsRepository) =
    inherit ApiController()
 
    new() = new SensorReadingsController(new SensorReadingsRepository())
 
    member x.Get() = 
        repository.GetAll()

    member x.Get(id : string, action : string, timestamp : string) = 
        // Routing table        
        if      action = "GetOne"      then repository.GetOne(id)
        elif    action = "GetAfter"    then repository.GetOneAfterTime(id, timestamp)
        else                                [||]
