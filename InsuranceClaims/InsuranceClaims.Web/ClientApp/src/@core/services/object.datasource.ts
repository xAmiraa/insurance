import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { Observable, BehaviorSubject, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { QueryParamsDto, ResponseDto } from "../models/common/SharedModels";
import { HttpService } from './http.service';

export class ObjectDataSource implements DataSource<any> {

  public data: any[] = [];

  public totalCount = 0;

  private objectsSubject = new BehaviorSubject<any[]>([]);

  private loadingSubject = new BehaviorSubject<boolean>(false);

  public loading$ = this.loadingSubject.asObservable();


  constructor(private httpService: HttpService) {

  }

  loadObjects(url: string, params: QueryParamsDto[] = []) {
    this.loadingSubject.next(true);
    this.httpService.GET(url, params).pipe(
      catchError(() => of([])),
      finalize(() => this.loadingSubject.next(false))
    )
      .subscribe(res => {
        let response = res as ResponseDto;
        if (response?.isPassed) {
          console.log(response)
          this.totalCount = response.data?.total;
          this.data = response.data?.list;
          this.objectsSubject.next(response.data?.list)
        }
      });
  }

  connect(collectionViewer: CollectionViewer): Observable<any[]> {
    console.log("Connecting data source");
    return this.objectsSubject.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    console.log("Disconnecting data source");
    this.objectsSubject.complete();
    this.loadingSubject.complete();
  }

}