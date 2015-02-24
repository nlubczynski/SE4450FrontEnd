namespace FsWeb.Controllers
 
open System.Web.Http
open FsWeb.Models
open FsWeb.Repositories
open System

type SensorReadingsController(repository : SensorReadingsRepository) =
    inherit ApiController()
 
    new() = new SensorReadingsController(new SensorReadingsRepository())
 
    member x.Get(lambda: bool) = 
        match lambda with
            | true -> 
                printfn ("Lambda is active1")
                repository.GetAll()
            | false ->
                printfn ("MySQL database is active1")
                repository.GetAll()

    member x.Get(id : string, action : string, timestamp : string, lambda: bool) = 
        // Routing table
        printfn("Selecting lambda or mysql: ")

        match lambda with 
            | true -> 
                printfn ("Lambda is active2")
                if      action = "GetOne"      then repository.GetOne(id)
                elif    action = "GetAfter"    then repository.GetOneAfterTime(id, timestamp)
                else    [||]
        
            | false -> 
                printfn ("MySQL database is active2")
                if      action = "GetOne"      then repository.GetOne(id)
                elif    action = "GetAfter"    then repository.GetOneAfterTime(id, timestamp)
                else    [||]
                
