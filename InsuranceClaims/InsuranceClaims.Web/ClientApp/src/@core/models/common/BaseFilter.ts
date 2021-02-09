export class BaseFilterDto {
  pageIndex?: number = 1;
  pageSize?: number = 5;
  applySort?: boolean = null;
  sortProperty?: string = null;
  isAscending?: boolean = true;
  isActive?: boolean = null;
}
