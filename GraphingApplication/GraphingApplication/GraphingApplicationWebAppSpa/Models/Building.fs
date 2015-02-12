namespace FsWeb.Models
 
open System
open System.ComponentModel.DataAnnotations

type Building() = 
    let mutable name = ""
    let mutable id = ""
    [<Key>] member x.Id with get() = id and set v = id <- v
    [<Required>] member x.Name with get() = name and set v = name <- v