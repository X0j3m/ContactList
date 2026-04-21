import { Component, signal, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ContactsService } from '../../services/contacts.service';
import { ContactModel } from '../../models/contact.model';

@Component({
  selector: 'app-contact',
  standalone: false,
  templateUrl: './contact.html',
  styleUrl: './contact.css',
})
export class Contact implements OnInit {
  public contact = signal<ContactModel | null>(null);

  constructor(
    private route: ActivatedRoute,
    private contactsService: ContactsService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;
    this.contactsService.getById(id).subscribe({
      next: (c) => this.contact.set(c),
      error: (err) => console.error('Błąd pobierania kontaktu:', err)
    });
  }
}
