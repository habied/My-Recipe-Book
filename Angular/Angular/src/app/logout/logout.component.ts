import { Component, NgZone } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})

export class LogoutComponent {

  constructor(private _ngZone: NgZone, private service:AuthService, private router:Router){}
  public logout(){
    this.service.signOutExternal();
    this._ngZone.run(()=>{
    this.router.navigate(['/']).then(()=>window.location.reload());
    })
  }
}
