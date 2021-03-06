export class States {
  public static List: State[] = [
    { code: 'AL', description: 'Alabama' },
    { code: 'AK', description: 'Alaska' },
    { code: 'AZ', description: 'Arizona' },
    { code: 'AR', description: 'Arkansas' },
    { code: 'AS', description: 'American Samoa' },
    { code: 'CA', description: 'California' },
    { code: 'CO', description: 'Colorado' },
    { code: 'CT', description: 'Connecticut' },
    { code: 'CT', description: 'Connecticut' },
    { code: 'DE', description: 'Delaware' },
    { code: 'DC', description: 'District of Columbia' },
    { code: 'FL', description: 'Florida' },
    { code: 'GA', description: 'Georgia' },
    { code: 'GU', description: 'Guam' },
    { code: 'HI', description: 'Hawaii' },
    { code: 'ID', description: 'Idaho' },
    { code: 'IL', description: 'Illinois' },
    { code: 'IN', description: 'Indiana' },
    { code: 'IA', description: 'Iowa' },
    { code: 'KA', description: 'Kansas' },
    { code: 'KY', description: 'Kentucky' },
    { code: 'LA', description: 'Louisiana' },
    { code: 'ME', description: 'Maine' },
    { code: 'MD', description: 'Maryland' },
    { code: 'MA', description: 'Massachusetts' },
    { code: 'MI', description: 'Michigan' },
    { code: 'MN', description: 'Minnesota' },
    { code: 'MS', description: 'Mississippi' },
    { code: 'MO', description: 'Missouri' },
    { code: 'MT', description: 'Montana' },
    { code: 'NE', description: 'Nebraska' },
    { code: 'NV', description: 'Nevada' },
    { code: 'NH', description: 'New Hampshire' },
    { code: 'NJ', description: 'New Jersey' },
    { code: 'NM', description: 'New Mexico' },
    { code: 'NY', description: 'New York' },
    { code: 'NC', description: 'North Carolina' },
    { code: 'ND', description: 'North Dakota' },
    { code: 'CM', description: 'Northern Mariana Islands' },
    { code: 'OH', description: 'Ohio' },
    { code: 'OK', description: 'Oklahoma' },
    { code: 'OR', description: 'Oregon' },
    { code: 'PA', description: 'Pennsylvania' },
    { code: 'PR', description: 'Puerto Rico' },
    { code: 'RI', description: 'Rhode Island' },
    { code: 'SC', description: 'South Carolina' },
    { code: 'SD', description: 'South Dakota' },
    { code: 'TE', description: 'Tennessee' },
    { code: 'TX', description: 'Texas' },
    { code: 'TT', description: 'Trust Territories' },
    { code: 'UT', description: 'Utah' },
    { code: 'VT', description: 'Vermont' },
    { code: 'VA', description: 'Virginia' },
    { code: 'VI', description: 'Virgin Islands' },
    { code: 'WA', description: 'Washington' },
    { code: 'WV', description: 'West Virginia' },
    { code: 'WI', description: 'Wisconsin' },
    { code: 'WY', description: 'Wyoming' },
  ]
}

export interface State {
  code: string | null;
  description: string;
}
