
import { environment } from 'src/environments/environment';

export const PolicyInsurersController = {
  SearchPolicyInsurers: environment.baseURL + `/api/policyinsurers`,
  CreatePolicyInsurer: environment.baseURL + `/api/policyinsurers`,
  UpdatePolicyInsurer: (id: number) => environment.baseURL + `/api/policyinsurers/${id}`,
  DeletePolicyInsurer: (id: number) => environment.baseURL + `/api/policyinsurers/${id}`,
}