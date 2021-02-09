import { BaseFilterDto } from "../common/BaseFilter";

export class CreateUpdateIdentificationTypeDto {
  name: string = null;
  description: string = null;
}

export class IdentificationTypeFilterDto extends BaseFilterDto {
  name: string = null;
}