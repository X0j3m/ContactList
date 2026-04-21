export interface ContactDto {
  id: string,
  name: string,
  surname: string,
  email: string,
  password: string,
  categoryId: string,
  subcategoryId: string,
  customSubCategory: string,
  phone: string
  birthDate: string
}

export interface CreateContactDto {
  name: string,
  surname: string,
  email: string,
  password: string,
  categoryId: string,
  subcategoryId: string,
  customSubCategory: string,
  phone: string
  birthDate: string
}

export interface UpdateContactDto {
  name: string,
  surname: string,
  email: string,
  password: string,
  categoryId: string,
  subcategoryId: string,
  customSubCategory: string,
  phone: string
  birthDate: string
}

export interface ContactModel {
  id: string,
  name: string,
  surname: string,
  email: string,
  password: string,
  category: string,
  subcategory: string,
  phone: string
  birthDate: string
}
