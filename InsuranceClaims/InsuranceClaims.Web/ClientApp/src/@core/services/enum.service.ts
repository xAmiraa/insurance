import { Injectable } from '@angular/core';
import * as enums from '../models/enum/Enums';
import { HumanizePipe } from '../pipes/humanizePipe';

@Injectable()
export class EnumService {

  humanizePipe: any;

  constructor() {
    this.humanizePipe = new HumanizePipe();
  }

  UserStatusEnum(value: string): EnumResult {
    let target = enums.UserStatusEnum;
    let result = new EnumResult();
    result.name = this.humanizePipe.transform(target[value]) || '-';

    switch (value) {
      case "Active":
        result.className = 'badge-success';
        break;
      case "NotActive":
        result.className = 'badge-danger';
        break;
      case "Locked":
        result.className = 'badge-warning';
        break;
      default:
        result.className = 'badge-default';
        break;
    }

    return result;
  }

}

class EnumResult {
  className: string = 'badge-default';
  name: string = '-';
}
