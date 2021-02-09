
import { environment } from 'src/environments/environment';

export const CompaniesController = {
  SearchCompanies: environment.baseURL + `/api/companies`,
  CreateCompany: environment.baseURL + `/api/companies`,
  UpdateCompany: (id: number) => environment.baseURL + `/api/companies/${id}`,
  DeleteCompany: (id: number) => environment.baseURL + `/api/companies/${id}`,
}