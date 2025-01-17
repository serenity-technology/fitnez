CREATE TABLE public.person (
  id UUID STORAGE PLAIN NOT NULL,
  name VARCHAR(100) STORAGE PLAIN NOT NULL,
  gender public.gender STORAGE PLAIN NOT NULL,
  date_of_birth DATE STORAGE PLAIN NOT NULL,
  id_passport VARCHAR(50) STORAGE PLAIN NOT NULL,
  address TEXT STORAGE PLAIN NOT NULL,
  is_trainer BOOLEAN STORAGE PLAIN NOT NULL,
  CONSTRAINT person_pkey PRIMARY KEY(id)
) ;

CREATE UNIQUE INDEX person_idx_id_passport ON public.person
  USING btree ((lower((id_passport)::text)) COLLATE pg_catalog."default");