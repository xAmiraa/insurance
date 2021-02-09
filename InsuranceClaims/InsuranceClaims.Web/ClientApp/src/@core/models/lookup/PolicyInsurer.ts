import { BaseFilterDto } from "../common/BaseFilter";

export class CreateUpdatePolicyInsurerDto {
  name: string = null;
  description: string = null;
}

export class PolicyInsurerFilterDto extends BaseFilterDto {
  name: string = null;
}