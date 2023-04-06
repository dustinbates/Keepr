export class Profile {
  constructor(data) {
    this.id = data.id
    this.name = data.name
    this.picture = data.picture
    // TODO add additional properties if needed
    this.coverImg = data.coverImg || 'https://oldnorthwestterritory.northwestquarterly.com/wp-content/uploads/2013/11/Home-CozyHome-Hol131.jpg'
  }
}
export class Account extends Profile{
  constructor(data){
    super(data)
      this.email = data.email
  }
}