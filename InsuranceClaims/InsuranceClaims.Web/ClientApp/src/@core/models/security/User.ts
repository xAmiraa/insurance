import { BaseFilterDto } from "../common/BaseFilter";
import { LookupDto } from "../common/Lookup";

export class StoredData {
  user: LoggedInUser = null;
  token: string = null;
}

export class LoggedInUser {
  id: number = null;
  personalImagePath?: string = null;
  name: string = null;
  email: string = null;
  firstName: string = null;
  lastName: string = null;
  address: string = null;
  groupName: string = null;
  role: LookupDto = new LookupDto();
}

export function getStoredData(): StoredData {
  return JSON.parse(localStorage.getItem('claim_user')) as StoredData;
}


export class CreateUpdateUserDto {
  email: string = null;
  firstName: string = null;
  lastName: string = null;
  address: string = null;
  groupName: string = null;
  roleId: number = null;
}

export class UserFilterDto extends BaseFilterDto {
  email: string = null;
  name: string = null;
  address: string = null;
  groupName: string = null;
  status: string = null;
}