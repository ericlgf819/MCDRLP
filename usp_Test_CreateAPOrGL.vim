let s:so_save = &so | let s:siso_save = &siso | set so=0 siso=0
argglobal
edit D:\projects\Mcdonalds\RLPlanning_sp_func_scripts.sql
let s:cpo_save=&cpo
set cpo&vim
imap <buffer> <Left> =sqlcomplete#DrillOutOfColumns()
imap <buffer> <Right> =sqlcomplete#DrillIntoTable()
vnoremap <buffer> <silent> [" :exec "normal! gv"|call search('\(^\s*\(--\|\/\/\|\*\|\/\*\|\*\/\).*\n\)\(^\s*\(--\|\/\/\|\*\|\/\*\|\*\/\)\)\@!', "W" )
nnoremap <buffer> <silent> [" :call search('\(^\s*\(--\|\/\/\|\*\|\/\*\|\*\/\).*\n\)\(^\s*\(--\|\/\/\|\*\|\/\*\|\*\/\)\)\@!', "W" )
vmap <buffer> <silent> [{ ?\c^\s*\(\(create\)\s\+\(or\s\+replace\s+\)\{,1}\)\{,1}\<\(function\|procedure\|event\|\(existing\|global\s\+temporary\s\+\)\{,1}table\|trigger\|schema\|service\|publication\|database\|datatype\|domain\|index\|subscription\|synchronization\|view\|variable\)\>
nmap <buffer> <silent> [{ :call search('\c^\s*\(\(create\)\s\+\(or\s\+replace\s+\)\{,1}\)\{,1}\<\(function\|procedure\|event\|\(existing\|global\s\+temporary\s\+\)\{,1}table\|trigger\|schema\|service\|publication\|database\|datatype\|domain\|index\|subscription\|synchronization\|view\|variable\)\>', 'bW')
vmap <buffer> <silent> [] :exec "normal! gv"|call search('\c^\s*end\W*$', 'bW' )
vmap <buffer> <silent> [[ :exec "normal! gv"|call search('\c^\s*begin\>', 'bW' )
nmap <buffer> <silent> [] :call search('\c^\s*end\W*$', 'bW' )
nmap <buffer> <silent> [[ :call search('\c^\s*begin\>', 'bW' )
vnoremap <buffer> <silent> ]" :exec "normal! gv"|call search('^\(\s*\(--\|\/\/\|\*\|\/\*\|\*\/\).*\n\)\@<!\(\s*\(--\|\/\/\|\*\|\/\*\|\*\/\)\)', "W" )
nnoremap <buffer> <silent> ]" :call search('^\(\s*\(--\|\/\/\|\*\|\/\*\|\*\/\).*\n\)\@<!\(\s*\(--\|\/\/\|\*\|\/\*\|\*\/\)\)', "W" )
vmap <buffer> <silent> ]} /\c^\s*\(\(create\)\s\+\(or\s\+replace\s+\)\{,1}\)\{,1}\<\(function\|procedure\|event\|\(existing\|global\s\+temporary\s\+\)\{,1}table\|trigger\|schema\|service\|publication\|database\|datatype\|domain\|index\|subscription\|synchronization\|view\|variable\)\>
nmap <buffer> <silent> ]} :call search('\c^\s*\(\(create\)\s\+\(or\s\+replace\s+\)\{,1}\)\{,1}\<\(function\|procedure\|event\|\(existing\|global\s\+temporary\s\+\)\{,1}table\|trigger\|schema\|service\|publication\|database\|datatype\|domain\|index\|subscription\|synchronization\|view\|variable\)\>', 'W')
vmap <buffer> <silent> ][ :exec "normal! gv"|call search('\c^\s*end\W*$', 'W' )
vmap <buffer> <silent> ]] :exec "normal! gv"|call search('\c^\s*begin\>', 'W' )
nmap <buffer> <silent> ][ :call search('\c^\s*end\W*$', 'W' )
nmap <buffer> <silent> ]] :call search('\c^\s*begin\>', 'W' )
imap <buffer> R :call sqlcomplete#Map("resetCache")
imap <buffer> L :call sqlcomplete#Map("column_csv")
imap <buffer> l :call sqlcomplete#Map("column_csv")
imap <buffer> c :call sqlcomplete#Map("column")
imap <buffer> v :call sqlcomplete#Map("view")
imap <buffer> p :call sqlcomplete#Map("procedure")
imap <buffer> t :call sqlcomplete#Map("table")
imap <buffer> s :call sqlcomplete#Map("sqlStatement")
imap <buffer> T :call sqlcomplete#Map("sqlType")
imap <buffer> o :call sqlcomplete#Map("sqlOption")
imap <buffer> f :call sqlcomplete#Map("sqlFunction")
imap <buffer> k :call sqlcomplete#Map("sqlKeyword")
imap <buffer> a :call sqlcomplete#Map("syntax")
let &cpo=s:cpo_save
unlet s:cpo_save
setlocal keymap=
setlocal noarabic
setlocal autoindent
setlocal balloonexpr=
setlocal nobinary
setlocal bufhidden=
setlocal buflisted
setlocal buftype=
setlocal nocindent
setlocal cinkeys=0{,0},0),:,0#,!^F,o,O,e
setlocal cinoptions=
setlocal cinwords=if,else,while,do,for,switch
setlocal colorcolumn=
setlocal comments=s1:/*,mb:*,ex:*/,:--,://
setlocal commentstring=/*%s*/
setlocal complete=.,w,b,u,t,i
setlocal concealcursor=
setlocal conceallevel=0
setlocal completefunc=
setlocal nocopyindent
setlocal cryptmethod=
setlocal nocursorbind
setlocal nocursorcolumn
setlocal nocursorline
setlocal define=\\c\\<\\(VARIABLE\\|DECLARE\\|IN\\|OUT\\|INOUT\\)\\>
setlocal dictionary=
setlocal nodiff
setlocal equalprg=
setlocal errorformat=
setlocal noexpandtab
if &filetype != 'sql'
setlocal filetype=sql
endif
setlocal foldcolumn=0
setlocal foldenable
setlocal foldexpr=0
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldmarker={{{,}}}
setlocal foldmethod=manual
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldtext=foldtext()
setlocal formatexpr=
setlocal formatoptions=qc
setlocal formatlistpat=^\\s*\\d\\+[\\]:.)}\\t\ ]\\s*
setlocal grepprg=
setlocal iminsert=2
setlocal imsearch=2
setlocal include=
setlocal includeexpr=
setlocal indentexpr=GetSQLIndent()
setlocal indentkeys=!^F,o,O,=~end,=~else,=~elseif,=~elsif,0=~when,0=)
setlocal noinfercase
setlocal iskeyword=@,48-57,_,128-167,224-235
setlocal keywordprg=
setlocal nolinebreak
setlocal nolisp
setlocal nolist
setlocal makeprg=
setlocal matchpairs=(:),{:},[:]
setlocal modeline
setlocal modifiable
setlocal nrformats=octal,hex
setlocal number
setlocal numberwidth=4
setlocal omnifunc=sqlcomplete#Complete
setlocal path=
setlocal nopreserveindent
setlocal nopreviewwindow
setlocal quoteescape=\\
setlocal noreadonly
setlocal norelativenumber
setlocal norightleft
setlocal rightleftcmd=search
setlocal noscrollbind
setlocal shiftwidth=4
setlocal noshortname
setlocal nosmartindent
setlocal softtabstop=0
setlocal nospell
setlocal spellcapcheck=[.?!]\\_[\\])'\"\	\ ]\\+
setlocal spellfile=
setlocal spelllang=en
setlocal statusline=
setlocal suffixesadd=
setlocal swapfile
setlocal synmaxcol=3000
if &syntax != 'sql'
setlocal syntax=sql
endif
setlocal tabstop=4
setlocal tags=
setlocal textwidth=0
setlocal thesaurus=
setlocal noundofile
setlocal nowinfixheight
setlocal nowinfixwidth
setlocal wrap
setlocal wrapmargin=0
silent! normal! zE
let s:l = 38721 - ((22 * winheight(0) + 20) / 40)
if s:l < 1 | let s:l = 1 | endif
exe s:l
normal! zt
38721
normal! 02l
let &so = s:so_save | let &siso = s:siso_save
doautoall SessionLoadPost
" vim: set ft=vim :
