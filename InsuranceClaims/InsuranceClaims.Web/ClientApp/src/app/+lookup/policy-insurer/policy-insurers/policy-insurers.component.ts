import { Component, ElementRef, OnInit, ViewChild, Injector } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fromEvent } from 'rxjs';
import { SelectionModel } from '@angular/cdk/collections';
import { ObjectDataSource } from 'src/@core/services/object.datasource';
import { takeUntil, tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { BaseService } from 'src/@core/services/base.service';
import { PolicyInsurerFilterDto } from 'src/@core/models/lookup/PolicyInsurer';
import { QueryParamsDto, TableColumn } from 'src/@core/models/common/SharedModels';
import { PolicyInsurersController } from 'src/@core/APIs/PolicyInsurersController';
import { AddEditPolicyInsurerComponent } from '../add-edit-policy-insurer/add-edit-policy-insurer.component';

@Component({
  selector: 'app-policy-insurers',
  templateUrl: './policy-insurers.component.html',
  styleUrls: ['./policy-insurers.component.scss']
})
export class PolicyInsurersComponent extends BaseService implements OnInit {

  filterDto: PolicyInsurerFilterDto = new PolicyInsurerFilterDto();

  // Table
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild('searchInput', { static: false }) searchInput: ElementRef;
  columns: TableColumn<any>[] = [
    { label: 'Actions', property: 'actions', type: 'button', visible: true },
    { label: 'Name', property: 'name', type: 'text', visible: true },
    { label: 'Description', property: 'description', type: 'text', visible: true },
    { label: 'Creator', property: 'creator', type: 'text', visible: true },
    { label: 'Created On', property: 'createdOn', type: 'datetime', visible: true },
    { label: 'Updator', property: 'updator', type: 'text', visible: false },
    { label: 'Updated On', property: 'updatedOn', type: 'datetime', visible: false },
  ];
  dataSource: ObjectDataSource;
  selection = new SelectionModel<any>(true, []);

  constructor(public injector: Injector) {
    super(injector);
  }


  ngOnInit() {
    this.dataSource = new ObjectDataSource(this.httpService);
  }

  ngAfterViewInit() {

    this.loadData();
    this.sort.sortChange.subscribe((res) => {
      this.paginator.pageIndex = 0;
      this.loadData();
    });

    this.paginator.page.pipe(tap(() => this.loadData())).subscribe();

    fromEvent(this.searchInput.nativeElement, 'keyup')
      .pipe(
        debounceTime(150),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;
          this.loadData();
        })
      )
      .subscribe();

  }

  toggleColumnVisibility(column, event) {
    event.stopPropagation();
    event.stopImmediatePropagation();
    column.visible = !column.visible;
  }

  trackByProperty<T>(index: number, column: TableColumn<T>) {
    return column.property;
  }

  get visibleColumns() {
    return this.columns.filter(column => column.visible).map(column => column.property);
  }

  loadData() {

    this.selection.clear();

    let params: QueryParamsDto[] = [
      { key: 'pageSize', value: this.paginator.pageSize || 10 },
      { key: 'pageIndex', value: this.paginator.pageIndex + 1 || 1 },
      { key: 'applySort', value: this.sort && this.sort.active ? true : false },
      { key: 'sortProperty', value: this.sort && this.sort.active ? this.sort.active : null },
      { key: 'isAscending', value: this.sort && this.sort.direction == 'asc' ? true : false },
      // Filter
      { key: 'name', value: this.searchInput.nativeElement.value || this.filterDto.name },
    ];

    // Append FilterDto values to the paramaters
    params = params.concat(this.getFilterParamsDtos(this.filterDto));

    this.dataSource.loadObjects(PolicyInsurersController.SearchPolicyInsurers, params);
  }
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    if (this.isAllSelected()) {
      this.selection.clear();
    } else {
      this.selection.clear();
      this.dataSource.data.forEach(row => this.selection.select(row));
    }
  }

  reset() {
    let skipped: string[] = [];

    if (this.filterDto != null) {
      Object.keys(this.filterDto).forEach(key => {
        if (!skipped.includes(key)) {
          this.filterDto[key] = null;
        }
      });

      // Reset page
      this.paginator.pageIndex = 0;
      // Reload the data
      this.loadData();
    }
  }

  add() {
    this.dialog.open(AddEditPolicyInsurerComponent)
      .afterClosed()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        if (res) {
          this.loadData();
        }
      });
  }

  update(policyInsurer: any) {
    this.dialog.open(AddEditPolicyInsurerComponent, {
      data: policyInsurer
    })
      .afterClosed()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        if (res) {
          this.loadData();
        }
      });
  }

  activate(policyInsurer: any) {

  }

  deactivate(policyInsurer: any) {

  }

  delete(policyInsurer: any) {
    this.sweetAlertService.Delete(PolicyInsurersController.DeletePolicyInsurer(policyInsurer.id))
      .then(x => {
        this.loadData();
      });
  }

}
