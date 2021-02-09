
import { environment } from 'src/environments/environment';

export const AccountController = {
  Login: environment.baseURL + `/api/login`,
  ResetPassword: environment.baseURL + `/api/resetpassword`,
  GetLoggedinUserInfo: environment.baseURL + `/api/information/me`,
  UpdateProfile: environment.baseURL + `/api/me/updateprofile`,
  UpdatePassword: environment.baseURL + `/api/me/changepassword`,

  SearchUsers: environment.baseURL + `/api/users`,
  CreateUser: environment.baseURL + `/api/users`,
  UpdateUser: (id: number) => environment.baseURL + `/api/users/${id}`,
  ActivateUser: (id: number) => environment.baseURL + `/api/users/${id}/activate`,
  DeactivateUser: (id: number) => environment.baseURL + `/api/users/${id}/deactivate`,
}