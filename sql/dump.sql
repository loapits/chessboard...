PGDMP     0    3            
    x            postgres    13.1    13.1     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    13442    postgres    DATABASE     e   CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';
    DROP DATABASE postgres;
                postgres    false            �           0    0    DATABASE postgres    COMMENT     N   COMMENT ON DATABASE postgres IS 'default administrative connection database';
                   postgres    false    3002                        3079    16384 	   adminpack 	   EXTENSION     A   CREATE EXTENSION IF NOT EXISTS adminpack WITH SCHEMA pg_catalog;
    DROP EXTENSION adminpack;
                   false            �           0    0    EXTENSION adminpack    COMMENT     M   COMMENT ON EXTENSION adminpack IS 'administrative functions for PostgreSQL';
                        false    2            �            1259    16404 
   chesscells    TABLE        CREATE TABLE public.chesscells (
    id integer NOT NULL,
    letters character varying(8),
    digits character varying(8)
);
    DROP TABLE public.chesscells;
       public         heap    postgres    false            �            1259    16402    chesscells_id_seq    SEQUENCE     �   CREATE SEQUENCE public.chesscells_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.chesscells_id_seq;
       public          postgres    false    202            �           0    0    chesscells_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.chesscells_id_seq OWNED BY public.chesscells.id;
          public          postgres    false    201            �            1259    16420    points    TABLE     �   CREATE TABLE public.points (
    id integer NOT NULL,
    point1 character varying(2),
    point2 character varying(2),
    result character varying(4)
);
    DROP TABLE public.points;
       public         heap    postgres    false            �            1259    16418    points_id_seq    SEQUENCE     �   CREATE SEQUENCE public.points_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.points_id_seq;
       public          postgres    false    204            �           0    0    points_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.points_id_seq OWNED BY public.points.id;
          public          postgres    false    203            )           2604    16426    chesscells id    DEFAULT     n   ALTER TABLE ONLY public.chesscells ALTER COLUMN id SET DEFAULT nextval('public.chesscells_id_seq'::regclass);
 <   ALTER TABLE public.chesscells ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    201    202    202            *           2604    16427 	   points id    DEFAULT     f   ALTER TABLE ONLY public.points ALTER COLUMN id SET DEFAULT nextval('public.points_id_seq'::regclass);
 8   ALTER TABLE public.points ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    203    204    204            �          0    16404 
   chesscells 
   TABLE DATA                 public          postgres    false    202   �       �          0    16420    points 
   TABLE DATA                 public          postgres    false    204   @       �           0    0    chesscells_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.chesscells_id_seq', 1, true);
          public          postgres    false    201            �           0    0    points_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.points_id_seq', 32, true);
          public          postgres    false    203            ,           2606    16409    chesscells chesscells_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.chesscells
    ADD CONSTRAINT chesscells_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.chesscells DROP CONSTRAINT chesscells_pkey;
       public            postgres    false    202            .           2606    16425    points points_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.points
    ADD CONSTRAINT points_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.points DROP CONSTRAINT points_pkey;
       public            postgres    false    204            �   `   x���v
Q���W((M��L�K�H-.NN��)V��L�Q�I-)I-*�QH�L�,)�Ts�	uV�0�QPOLJNIMK�P���ML��-�5���� *�j      �   $  x������0��}�ܪ �3��œ�����]��De�������sI����&����k��z�U����7�ky�դ73%���������1U?����NM�O��am�[�m:]%����9�=�(I	�\��X��C�@��}�o&����������	�eT�(e�Ťy
;%� ��(�l�)J:B���h��?�O�:I�[�m�2F��C;���!j��ΪF6���ʣ;�`�`��1�F��8�U�(l����0h�
��Sz��I��~J��N*3�UĨ��;�h�� �Y8     