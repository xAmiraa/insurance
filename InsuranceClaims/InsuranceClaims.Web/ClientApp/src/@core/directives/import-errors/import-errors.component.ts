import { Component, Inject, OnInit, Injector, EventEmitter, Output, Input } from '@angular/core';
import { FormValidationError } from 'src/@core/models/common/SharedModels';
import { BaseService } from '../../services/base.service';

@Component({
  selector: 'import-errors',
  templateUrl: './import-errors.component.html',
  styleUrls: ['./import-errors.component.scss']
})
export class ImportErrorsComponent extends BaseService implements OnInit {

  @Output('notify') notify = new EventEmitter<number>();
  @Input('validationErrors') validationErrors: FormValidationError[] = [];
  @Input('pageSize') pageSize: number = 0;

  constructor(public injector: Injector) {
    super(injector);
  }

  ngOnInit() {

  }

  paginateToError(err: FormValidationError) {
    let pageNumber = Math.ceil(err.rowNumber / this.pageSize);
    let pageIndex = pageNumber -1;
    console.log(this.pageSize, pageIndex); 
    
    this.notify.emit(pageIndex);
  }

}
