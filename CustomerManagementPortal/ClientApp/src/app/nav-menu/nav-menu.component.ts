import { Component } from '@angular/core';

import { AccountService } from 'src/app/_services';
import { User } from 'src/app/_models';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  user: User;
  isExpanded = false;

  constructor(private accountService: AccountService) {
    this.accountService.user.subscribe(x => this.user = x);
  }

  logout() {
    this.accountService.logout();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
