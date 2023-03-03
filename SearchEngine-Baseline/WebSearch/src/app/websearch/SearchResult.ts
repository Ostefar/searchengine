import { MyDocument } from "./MyDocument";

export interface SearchResult{
  elapsedMilliseconds: number;
  ignoredTerms: string[];
  documents: MyDocument[];

}
