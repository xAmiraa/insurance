export class FileExtIcon {
  iconPath: string = null;
  supportedExt: string[] = [];
}

export class KeyValueObject {
  id: number;
  name: string;
}

export class FormValidationError {
  message: string;
  rowNumber: number;
}

export class ResponseDto {
  isPassed: boolean;
  message: string;
  data: any;
  errors: string[];
}

export class QueryParamsDto {
  key: string;
  value: any;
}

export interface TableColumn<T> {
  label: string;
  property: keyof T | string;
  type: 'text' | 'image' | 'badge' | 'progress' | 'checkbox' | 'button' | 'date' | 'datetime' | 'price' | 'number' | 'int' | 'percent' | 'bool' | 'custom' | 'select';
  visible?: boolean;
  cssClasses?: string[];
}
