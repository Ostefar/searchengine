import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { DataInterface } from './DataInterface';
import { Service } from './service.component';

@Component({
  selector: 'app-websearch',
  templateUrl: './websearch.component.html',
  styleUrls: ['./websearch.component.css']
})

export class WebsearchComponent {
  hits!: Observable<any>;
  results!: Observable<any>[];
  timeUsed!: Observable<any>;
  hasBeenClicked: boolean = false;
  constructor(private service: Service)
  {
   
  }

  search()
  {
    var searchTerms = (<HTMLInputElement>document.getElementById("search")).value;
    if (searchTerms != null) {
      this.service.search(searchTerms)
        .subscribe(
          (data) => {
            console.log('Search submitted successfully');
           /*this.hasBeenClicked = true;
            this.hits = data.documents.length;
            this.timeUsed = data.elapsedMilliseconds;
            this.results.removeAll();
            data.documents.forEach(function(hit) {
              this.results.push(hit);*/
            });

            console.log(this.hits)
            //this.results = data;
            //this.test = data[0].documentsCount;
            //console.log(this.results);
            //console.log(this.test);
            // create a loop that prints out the data values
            //data.forEach()
          },
          (error: HttpErrorResponse) => {
            console.log(error);
          }
        );
    }
  }

}
