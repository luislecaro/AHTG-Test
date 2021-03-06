import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public isAuthenticated?: Observable<boolean>;
  public userName?: Observable<string | null | undefined>;
  public dateTime: string;

  constructor(private authorizeService: AuthorizeService) {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    this.dateTime = new Date().toLocaleDateString();
  }

}
