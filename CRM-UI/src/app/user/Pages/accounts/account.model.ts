import { ContactDetails } from '../../Models/ContactDetails.models';
import { Contact } from '../contacts/contact.model';

export interface Account {
  accountId: string;
  name: string;
  websiteUrl: string;
  description: string;
  fax: string;
  contacts: Contact[];
  creditLimit: number;
  revenue: number;
  tickerSymbol: string;
  Address: string;
  imageUrl: string;
  primaryContact: ContactDetails
}
