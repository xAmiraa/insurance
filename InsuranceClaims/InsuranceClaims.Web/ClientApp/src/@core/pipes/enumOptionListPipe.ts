import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumOptionlist'
})
export class EnumOptionListPipe implements PipeTransform {
  transform(value: any, showEmptyOption: boolean = false): any {
    let items: any[] = [];
    if (showEmptyOption) {
      items.push({ id: '', name: '---choose---' });
    }
    for (let key in value) {
      var isValueProperty = parseInt(key, 10) >= 0;
      if (!isValueProperty) continue;
      items.push({ id: Number(key), name: value[key] });
    }
    return items;
  }
}

// How to use it
// <option *ngFor="let option of (YourEnum | enumOptionlist: true)" [value]="option.id">{{
//   option.name
// }}</option>