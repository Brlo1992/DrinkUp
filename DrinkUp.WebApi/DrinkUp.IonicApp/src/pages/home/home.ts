import { Component } from "@angular/core";
import { NavController } from 'ionic-angular';
@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  constructor(public navCtrl: NavController) {
    // If we navigated to this page, we will have an item available as a nav param
  }

  onLink(url: string) {
      window.open(url);
  }
}
