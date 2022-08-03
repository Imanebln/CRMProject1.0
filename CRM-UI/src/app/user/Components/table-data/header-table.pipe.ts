import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'headerTable',
})
export class HeaderTablePipe implements PipeTransform {
  transform(value: string, ...args: unknown[]) {
    if (value == 'email') {
      return 'Email';
    } else if (value == 'birthdate') {
      return 'Birth Date';
    } else if (value == 'jobTitle') {
      return 'Job Title';
    }
    return value;
  }
}
