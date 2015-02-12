﻿namespace FsWeb.Controllers
 
open System.Web.Http
open FsWeb.Models
open FsWeb.Repositories
 
type SensorsController(repository : SensorsRepository) =
    inherit ApiController()
 
    new() = new SensorsController(new SensorsRepository())
 
    member x.Get() = 
        repository.GetAll()
