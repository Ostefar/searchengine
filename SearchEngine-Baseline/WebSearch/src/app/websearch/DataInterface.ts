export interface DataInterface {
  id: number;
  path: string;
  numberOfOccurences: number;
  elapsedMilliseconds: number;
  ignoredTerms: string[];
  documents: Document[];
}
