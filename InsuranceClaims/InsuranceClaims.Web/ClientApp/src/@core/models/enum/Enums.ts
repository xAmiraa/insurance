export enum UserStatusEnum {
  Active,
  NotActive,
  Locked
}
export enum ApplicationRolesEnum {
  SuperAdmin = 1,
  LocalAdmin = 2,
  Manager = 3,
  Standard = 4
}
export enum ClaimStatusesEnum {
  Pending = 1,
  PendingWithQuery = 2,
  Cancelled = 3,
  Completed = 4
}
export enum CoverageTypesEnum {
  EmployeeOnly = 1,
  EmployeeAndChild = 2,
  EmployeeAndSouse = 3,
  EmployeeAndFamily = 4
}