namespace FsWeb.Repositories
 
open System.Data.Entity
open FsWeb.Models
 
type BuildingsContext() =
    inherit DbContext("BuildingEntities")
 
    do Database.SetInitializer(new CreateDatabaseIfNotExists<BuildingsContext>())
 
    [<DefaultValue()>]
    val mutable buildings : IDbSet<Building>
 
    member x.Contacts with get() = x.buildings and set v = x.buildings <- v
 
type BuildingsRepository() =
    
    member x.GetAll() =
 
        use context = new BuildingsContext()
        context.buildings |> Seq.toList
 
    member x.AddNew(building) =
 
        use context = new BuildingsContext()
        context.buildings.Add(building) |> ignore
        context.SaveChanges()