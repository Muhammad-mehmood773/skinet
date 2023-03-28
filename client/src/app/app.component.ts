import { Component, OnInit } from '@angular/core';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';

  constructor () {}

  ngOnInit(): void { 
     
    }
  }
  
 // this.http.get<Pagination<Product[]>>('https://localhost:5001/api/products?pageSize=1').subscribe({
      //   next: response => this.products = response.data, // what to do next
      //   error: error => console.log(error), //what to do id there is an error
      //   complete: () => {
      //     console.log("Request Completed");
      //     console.log("extra Statment");
      //   }
      // })