module CleverClass.Domain

type ClassGroupDto = { Id : int
                       Name : string }
                       
let mapClassGroupToDto (classGroup : Db.ClassGroup) = { Id = classGroup.Id 
                                                        Name = classGroup.Name }                                                        
                                                        
let getClassGroups () = Db.getClassGroups ()
                        |> List.map mapClassGroupToDto
                        
let createClassGroup (classGroup : ClassGroupDto) = classGroup.Name
                                                    |> Db.createClassGroup
                                                    |> mapClassGroupToDto