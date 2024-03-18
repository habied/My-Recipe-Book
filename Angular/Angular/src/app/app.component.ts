import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'angular-one-tap-signin';
  token: string | null = null; // Initialize token as null or with a default value

  constructor() {}

  ngOnInit() {
    this.token = localStorage.getItem("accessToken");
    console.log(this.token);
  }
}
