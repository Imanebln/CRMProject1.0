import { Contact } from '../contacts/contact.model';

export interface Account {
  accountId: string;
  name: string;
  websiteUrl: string;
  description: string;
  fax: string;
  contacts: Contact[];
}
