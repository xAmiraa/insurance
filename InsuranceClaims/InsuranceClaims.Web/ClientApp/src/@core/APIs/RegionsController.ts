import { environment } from 'src/environments/environment';

export const RegionsController = {
    GetAll: environment.baseURL + `/api/Regions/GetAll`,
    GetAllAsDrp: environment.baseURL + `/api/Regions/GetAllAsDrp`,
    GetRegionDetails: environment.baseURL + `/api/Regions/GetRegionDetails`,
    CreateRegion: environment.baseURL + `/api/Regions/CreateRegion`,
    UpdateRegion: environment.baseURL + `/api/Regions/UpdateRegion`,
    UpdateIsActive: environment.baseURL + `/api/Regions/UpdateIsActive`,
    UpdateIsActiveForSelected: environment.baseURL + `/api/Regions/UpdateIsActiveForSelected`,
    RemoveRegion: environment.baseURL + `/api/Regions/RemoveRegion`,
    ImportRegions: environment.baseURL + `/api/Regions/ImportRegions`,
    ExportRegions: environment.baseURL + `/api/Regions/ExportRegions`,
}