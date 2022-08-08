import { Address } from "./Address.models";

export interface ContactDetails {
    contactId: string;
    firstname: string;
    lastname: string;
    birthdate: Date;
    email: string;
    fax: string;
    jobTitle: string;
    isPrimary: boolean;
    mobilePhone: string;
    addresses: Address[];
    imageUrl: string;
  }
  