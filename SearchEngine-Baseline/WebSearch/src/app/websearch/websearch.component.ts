import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DataInterface } from './DataInterface';
import { Service } from './service.component';

@Component({
  selector: 'app-websearch',
  templateUrl: './websearch.component.html',
  styleUrls: ['./websearch.component.css']
})

export class WebsearchComponent {
  results: DataInterface[] = [];
  test: number = 0;
  constructor(private service: Service)
  {
   
  }

  search()
  {
    var searchTerms = (<HTMLInputElement>document.getElementById("search")).value;
    if (searchTerms != null) {
      this.service.search(searchTerms)
        .subscribe(
          (data: any) => {
            console.log('Search submitted successfully');
            this.results = data;
            for (let result of this.results) {
              result.elapsedMilliseconds = this.test;
            }
            console.log(this.test)
            // create a loop that prints out the data values
            //data.forEach()
          },
          (error: HttpErrorResponse) => {
            console.log(error);
          }
        );
    }
  }

  getSearchResults()
  {
  }

}
