import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'keys',
})
export class KeysPipe implements PipeTransform {
  transform(value, ...args: unknown[]): any {
    let banned: string[] = [
      'contactId',
      'account',
      'isPrimary',
      'imageUrl',
      'birthdateObj',
      'addresses',
    ];
    let keys: any = [];
    for (let key in value) {
      if (banned.includes(key)) {
        continue;
      }
      keys.push({ key: key, value: value[key] });
    }
    return keys;
  }
}
