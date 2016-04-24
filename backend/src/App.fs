module CleverClass.App

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

let jsonSerializerSettings = new JsonSerializerSettings()
jsonSerializerSettings.ContractResolver <- new CamelCasePropertyNamesContractResolver()

let jsonWebPart obj = JsonConvert.SerializeObject(obj, jsonSerializerSettings)
                         |> OK
                         >=> Writers.setMimeType "application/json; charset=utf-8" 

let getClassGroups = warbler ( fun _ -> Domain.getClassGroups ()
                                        |> jsonWebPart)

let fromJson<'a> json = JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a                   
                                        
let getResourceFromRequest<'a> (request : HttpRequest) = 
        let getString rawForm = System.Text.Encoding.UTF8.GetString(rawForm)
        request.rawForm |> getString |> fromJson<'a>                                        

let routes = 
    choose 
        [ GET >=> choose 
            [ path "/classgroups" >=> getClassGroups ]
          POST >=> choose 
            [ path "/classgroups" >=> request (getResourceFromRequest >> Domain.createClassGroup >> jsonWebPart) ] ]

startWebServer defaultConfig routes