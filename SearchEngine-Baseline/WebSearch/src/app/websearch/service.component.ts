import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})

export class Service {
  searchTerms: string = "";
  readonly APIUrl = "http://localhost:5035/Search?terms=";

  constructor(private http: HttpClient) { }

  search(payload: any): Observable<any> {
    return this.http.get(this.APIUrl + payload + "&numberOfResults=10");
  }
}
