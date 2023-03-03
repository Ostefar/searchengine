import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { DataInterface } from './DataInterface';
import { MyDocument } from './MyDocument';
import { SearchResult } from './SearchResult';
import { Service } from './service.component';

@Component({
  selector: 'app-websearch',
  templateUrl: './websearch.component.html',
  styleUrls: ['./websearch.component.css']
})

export class WebsearchComponent {
  results!: SearchResult;
  hits!: number;
  timeUsed!: number;
  hasBeenClicked: boolean = false;

  constructor(private service: Service) { }

  search()
  {
    let me = this;
    var searchTerms = (<HTMLInputElement>document.getElementById("search")).value;
    if (searchTerms != null) {
      this.service.search(searchTerms)
        .subscribe(
          (data: SearchResult) => {
            this.results = data;
            this.hasBeenClicked = true;
            this.timeUsed = data.elapsedMilliseconds;
            this.hits = this.results.documents.length;
          },
          (error: HttpErrorResponse) => {
            console.log(error);
          }
        );
    }
  }

}
