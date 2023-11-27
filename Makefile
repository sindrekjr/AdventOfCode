define newline


endef

LANG ?= cs
YEAR ?= $(shell date +%Y)
DAYS ?= $(shell seq 1 25)

TEMPLATE := $(subst <YEAR>,$(YEAR),$(subst $(newline),\n,$(file < templates/solution.$(LANG).template)))

ifeq ($(LANG), cs)
	DIRECTORY := "$(shell pwd)/AdventOfCode.Solutions/Year$(YEAR)"
else ifeq ($(LANG), rs)
	DIRECTORY := "$(shell pwd)/AdventOfCode.Solutions.Rust/src/y$(YEAR)"
endif

solutions-files: $(DAYS)

$(DAYS):
	$(eval DAY=$(shell printf "%02d" $@))
ifeq ($(LANG), cs)
	$(eval DAY_DIR="$(DIRECTORY)/Day$(DAY)")
	$(eval DAY_FILE="$(DAY_DIR)/Solution.cs")
else ifeq ($(LANG), rs)
	$(eval DAY_DIR="$(DIRECTORY)")
	$(eval DAY_FILE="$(DIRECTORY)/d$(DAY).rs")
endif
	@if [ ! -f $(DAY_FILE) ]; then \
		mkdir -p $(DAY_DIR); \
		echo '$(subst <DAY>,$(DAY),$(TEMPLATE))' > $(DAY_FILE); \
	fi
