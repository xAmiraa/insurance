import { BaseEntityDto } from "../common/BaseEntityDto";
import { BaseFilterDto } from "../common/BaseFilter";

export class RegionDto extends BaseEntityDto {
  countryId: number = null;
  shortName: string = null;

  // UI
  countryName: string = null;
  countryFlag: string = null;
}


export class RegionFilterDto extends BaseFilterDto {
  name: string = null;
  countryId: number = null;
  shortName: string = null;
}