import { environment } from 'src/environments/environment';

export const CountriesController = {
    GetAll: environment.baseURL + `/api/Countries/GetAll`,
    GetAllAsDrp: environment.baseURL + `/api/Countries/GetAllAsDrp`,
    GetCountryDetails: environment.baseURL + `/api/Countries/GetCountryDetails`,
    CreateCountry: environment.baseURL + `/api/Countries/CreateCountry`,
    UpdateCountry: environment.baseURL + `/api/Countries/UpdateCountry`,
    UpdateIsActive: environment.baseURL + `/api/Countries/UpdateIsActive`,
    UpdateIsActiveForSelected: environment.baseURL + `/api/Countries/UpdateIsActiveForSelected`,
    RemoveCountry: environment.baseURL + `/api/Countries/RemoveCountry`,
    ImportCountries: environment.baseURL + `/api/Countries/ImportCountries`,
    ExportCountries: environment.baseURL + `/api/Countries/ExportCountries`,
}