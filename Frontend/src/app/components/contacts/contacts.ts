import { Component, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { ContactsService } from '../../services/contacts.service'
import { ContactDto, ContactModel } from '../../models/contact.model';
import { LoginService } from '../../services/login.service'

@Component({
  selector: 'app-contacts',
  standalone: false,
  templateUrl: './contacts.html',
  styleUrls: ['./contacts.css'],
})
export class Contacts {
  public contacts = signal<ContactModel[]>([]);
  public isLoggedIn = computed(() => this.loginService.loggedIn());

  constructor(
    private contactsService: ContactsService,
    private loginService: LoginService,
    private router: Router,
  ) {

    this.contactsService.getAll().subscribe({
      next: (contacts) => {
        this.contacts.set(contacts);
        console.log(contacts);
      },
      error: (err) => console.error('Błąd:', err)
    });
  }

  public details(contact: ContactModel) {
    if (!contact || !contact.id) return;
    this.router.navigate(['/contact', contact.id]);
  }

  public trackById(index: number, item: ContactModel) {
    return item.id;
  }

  public loggedIn(): boolean {
    return this.loginService.loggedIn();
  }
}
