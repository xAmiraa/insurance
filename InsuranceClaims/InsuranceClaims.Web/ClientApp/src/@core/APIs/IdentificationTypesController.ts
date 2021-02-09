
import { environment } from '../../environments/environment';

export const IdentificationTypesController = {
  SearchIdentificationTypes: environment.baseURL + `/api/identificationtypes`,
  CreateIdentificationType: environment.baseURL + `/api/identificationtypes`,
  UpdateIdentificationType: (id: number) => environment.baseURL + `/api/identificationtypes/${id}`,
  DeleteIdentificationType: (id: number) => environment.baseURL + `/api/identificationtypes/${id}`,
}
