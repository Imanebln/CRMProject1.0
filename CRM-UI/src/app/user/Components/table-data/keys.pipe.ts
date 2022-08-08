import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'keys',
})
export class KeysPipe implements PipeTransform {
  transform(value, ...args: unknown[]): any {
    let keys: any = [];
    for (let key in value) {
      if (key == 'account' || key == 'contactId' || key == 'birthdateObj') {
        continue;
      }
      keys.push({ key: key, value: value[key] });
    }
    return keys;
  }
}
